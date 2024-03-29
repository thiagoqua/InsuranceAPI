﻿using InsuranceAPI.Exceptions;
using InsuranceAPI.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using InsuranceAPI.Repositories;
using NPOI.XWPF.UserModel;
using Org.BouncyCastle.Crypto.Parameters;
using InsuranceAPI.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.SS.Formula.Functions;
using NPOI.HPSF;
using Microsoft.IdentityModel.Tokens;

namespace InsuranceAPI.Helpers {
    public class ExcelHandler {
        private readonly IFormFile? file;
        private readonly List<Company>? companies;
        private readonly List<Producer>? producers;
        private readonly string filesDir;

        public ExcelHandler() {
            filesDir = Path.Combine(Environment.CurrentDirectory,"Files");
        }
        
        public ExcelHandler(IFormFile formFile,List<Producer> prods,
                                List<Company> comps) : this() {
            file = formFile;
            producers = prods;
            companies = comps;
        }

        public async Task<ExcelDataResultDTO> parse() {
            List<Insured> interpreted = new List<Insured>();
            List<string> nonInterpretedRows = new List<string>();
            Stream stream = file!.OpenReadStream();
            IWorkbook workBook;
            ISheet sheet;
            IRow row;
            int rows;
            string extension = Path.GetExtension(file.FileName);

            if(extension == ".xlsx")
                workBook = new XSSFWorkbook(stream);
            else
                workBook = new HSSFWorkbook(stream);

            sheet = workBook.GetSheetAt(0);
            rows = sheet.LastRowNum;

            await Task.Run(() => {
                //modify the i starting value for the corresponding first row of data
                for(int i = 1; i < rows; i++) {
                    row = sheet.GetRow(i);
                    try {
                        interpreted.Add(mapFromExcelRow(row, i));
                    } catch(MappingException mex) {
                        nonInterpretedRows.Add(
                            "MAPPING ERROR ON ROW " + (mex.ErrorRow + 1).ToString() +
                            " IN CELL " + mex.ErrorCell.ToString()
                        );
                    } catch(Exception ex) {
                        nonInterpretedRows.Add(
                            "ERROR ON ROW " + i +
                            " OF TYPE " + ex.Message
                        );
                    }
                }
            });

            stream.Close();

            return new ExcelDataResultDTO(interpreted, nonInterpretedRows);
        }

        public async Task<byte[]?> export(List<Insured> insureds) {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("asegurados");
            byte[]? fileBytes = null;

            await Task.Run(() => {
                setColumnsNames(sheet.CreateRow(0),
                                workbook.CreateCellStyle(),
                                workbook.CreateFont());

                for(int i = 1;i < insureds.Count; ++i)
                    setColumnData(sheet.CreateRow(i), 
                                  insureds[i-1],
                                  workbook.CreateCellStyle());
            });

            using(MemoryStream stream = new()) {
                workbook.Write(stream, true);
                fileBytes = stream.ToArray();
            }

            return fileBytes;
        }

        private Insured mapFromExcelRow(IRow row, int rowNumber) {
            try{
                string[]? namesPolicy = mapNameOrPolicyFromExcelRow(row.GetCell(4).ToString());
                Company company = mapCompanyFromExcelRow(row.GetCell(1).ToString());
                Producer producer = mapProducerFromExcelRow(row.GetCell(14).ToString());
                Insured ret = new Insured(){
                    License = mapStringFromExcelRow(row.GetCell(0).ToString()),
                    Folder = mapFolderFromExcelRow(row.GetCell(2).ToString()),
                    Life = mapStringFromExcelRow(row.GetCell(3).ToString()),
                    Firstname = namesPolicy[0],
                    Lastname = namesPolicy[1],
                    InsurancePolicy = namesPolicy[2],
                    Born = mapBornFromExcelRow(row.GetCell(5).ToString()),
                    AddressNavigation = mapAddressFromExcelRow(row),
                    Dni = mapDNIFromExcelRow(row.GetCell(10).ToString()),
                    Phones = mapPhonesFromExcelRow(row.GetCell(11).ToString()),
                    Description = mapDescriptionFromExcelRow(row.GetCell(12).ToString()),
                    Cuit = row.GetCell(13).ToString(),
                    Producer = producer.Id,
                    ProducerNavigation = producer,
                    CompanyNavigation = company,
                    Company = company.Id,
                    Status = mapStringFromExcelRow(row.GetCell(7).ToString()),
                    PaymentExpiration = mapPaymentExpirationFromExcelRow(row.GetCell(8).ToString())
                };
                return ret;
            }
            catch(MappingException me){
                me.ErrorRow = rowNumber;
                throw me;
            }
        }

        private string mapStringFromExcelRow(string? cell) {
            if(cell != null)
                return cell;
            throw new MappingException("license");
        }

        private int mapFolderFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("folder");

            try{
                return int.Parse(cell);
            }
            catch(Exception){
                throw new MappingException("folder");
            }
        }

        private DateTime mapBornFromExcelRow(string? cell) {
            if(cell != null)
                return DateTime.Parse(cell);
            throw new MappingException("born");
        }

        private string mapDNIFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("DNI");

            if(cell.StartsWith("DNI "))
                return cell.Split(" ")[1];
            else if(cell.StartsWith("LE "))
                return cell.Split(" ")[1];

            throw new MappingException("DNI");
        }

        private string? mapDescriptionFromExcelRow(string? cell) {
            if(cell != null)
                return cell.Replace("(", "").Replace(")", "");
            return cell;
        }

        private string[] mapNameOrPolicyFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("name");

            string[] completeNameAndPolicy = cell.Split(" ");
            int policyIndex = -1;

            if(completeNameAndPolicy.Length < 2)
                throw new MappingException("name");

            for(int i = 0; i < completeNameAndPolicy.Length; ++i)
            {
                //becouse the description is inside braces
                if(completeNameAndPolicy[i].StartsWith("("))
                {
                    policyIndex = i;
                    break;
                }
            }

            //setting the first name
            string completeFirstName = completeNameAndPolicy[1];
            for(int i = 2; i < policyIndex; ++i)
                completeFirstName += " " + completeNameAndPolicy[i];
            //setting the last name
            string lastname = completeNameAndPolicy[0];
            //setting the policy if exists
            string? policy;
            if(policyIndex != -1)
            {
                string aux = completeNameAndPolicy[policyIndex];
                for(int i = policyIndex + 1; i < completeNameAndPolicy.Length; ++i)
                    aux += " " + completeNameAndPolicy[i];
                policy = aux.Replace("(", "").Replace(")", "");
            }
            else
                policy = null;

            return new string[] { completeFirstName, lastname, policy };
        }
        /// <summary>
        ///     It receives 
        /// </summary>
        /// <param name="row">Excel document's row</param>
        /// <returns>An address with the data in the row</returns>
        /// <exception cref="MappingException">
        ///     If cell 5 doesn't have the format: _street _number P _floor DTO _dpt.
        /// </exception>
        private Models.Address mapAddressFromExcelRow(IRow row) {
            string[]? cell = row.GetCell(6).ToString()?.Split(" ");
            string? street, number, city, province, country, departament;
            int numberStartIndex, floor;

            city = mapStringFromExcelRow(row.GetCell(9).ToString());
            province = "Santa Fe";
            country = "Argentina";
            departament = null;
            numberStartIndex = floor = -1;

            if(cell == null || cell.Length < 2)
                throw new MappingException("dirección");

            //getting the number
            for(int i = 0; i < cell.Length; ++i){
                if(!string.IsNullOrEmpty(cell[i]) && (char.IsDigit(cell[i], 0) || 
                                                        cell[i].Equals("S/N"))){
                    numberStartIndex = i;
                    break;
                }
            }

            if(numberStartIndex == -1)
                throw new MappingException("número_dirección");

            //getting the floor and departament if exists
            try{
                for(int i = numberStartIndex + 1; i < cell.Length; ++i){
                    if(cell[i].Equals("P"))
                        floor = int.Parse(cell[i + 1]);
                    else if(cell[i].Equals("DTO"))
                        departament = cell[i + 1];
                }
            }
            catch(Exception){
                throw new MappingException("piso_departamento");
            }

            //setting values
            //all the strings before the number are the street
            street = "";
            for(int i = 0; i < numberStartIndex; ++i)
                street += " " + cell[i];

            //all the strings between the number and the floor or departament are the number
            number = "";
            for(int i = numberStartIndex; i < cell.Length; ++i)
            {
                if(!cell[i].Equals("P") && !cell[i].StartsWith("DTO"))
                    number += " " + cell[i];
                else
                    break;
            }

            return new Models.Address(){
                Street = street,
                Number = number,
                Floor = floor != -1 ? floor : null,
                Departament = departament,
                City = city,
                Province = province,
                Country = country
            };
        }

        private List<Phone> mapPhonesFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("phones");

            List<Phone> ret = new List<Phone>();
            string[] phones = cell.Split('/');
            Phone toAdd;

            foreach(string phone in phones){
                //contains a description
                if(phone.Contains("(")){
                    string[] numberAndDescription = phone.Split("(");
                    toAdd = new Phone()
                    {
                        Number = numberAndDescription[0],
                        Description = numberAndDescription[1]
                                        .Replace("(", "")
                                        .Replace(")", "")
                    };
                }
                else
                    toAdd = new Phone(){
                        Number = phone
                    };
                ret.Add(toAdd);
            }
            return ret;
        }

        private Producer mapProducerFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("producer");

            Producer? ret = producers!.Find(prod => 
                prod.Firstname == cell.ToLower() ||
                prod.Lastname == cell.ToLower()
            );

            if(ret == null)
                throw new MappingException("producer");

            return ret;
        }

        private Company mapCompanyFromExcelRow(string? cell) {
            Company? ret = null;
            
            if(cell == null)
                throw new MappingException("company");
            
            //if the cell's color is white, the company is Federación
            if(cell == "FEDPAT")
                ret = companies!.Find(comp => comp.Id == 2);
            else if(cell == "COOP")
                ret = companies!.Find(comp => comp.Id == 1);

            if(ret == null)
                throw new MappingException("company");

            return ret;
        }

        private short mapPaymentExpirationFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("payment_expiration");

            return short.Parse(cell);
        }

        private void setColumnsNames(IRow row,ICellStyle styles,IFont font) {
            NPOI.SS.UserModel.ICell cell;

            font.IsBold = true;
            font.Underline = FontUnderlineType.Double;
            font.FontHeightInPoints = 14;
            font.Color = NPOI.HSSF.Util.HSSFColor.DarkBlue.Index;

            styles.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
            styles.FillPattern = FillPattern.SolidForeground;
            styles.BorderBottom = BorderStyle.Medium;
            styles.SetFont(font);

            cell = row.CreateCell(0);
            cell.SetCellValue("MATRICULA");

            cell = row.CreateCell(1);
            cell.SetCellValue("CARPETA");

            cell = row.CreateCell(2);
            cell.SetCellValue("VIGENCIA");

            cell = row.CreateCell(3);
            cell.SetCellValue("CLIENTE");

            cell = row.CreateCell(4);
            cell.SetCellValue("FECHA NAC.");

            cell = row.CreateCell(5);
            cell.SetCellValue("DIRECCION");

            cell = row.CreateCell(6);
            cell.SetCellValue("ESTADO");

            cell = row.CreateCell(7);
            cell.SetCellValue("VTO. CUOTA");

            cell = row.CreateCell(8);
            cell.SetCellValue("LOCALIDAD");

            cell = row.CreateCell(9);
            cell.SetCellValue("DOCUMENTO");

            cell = row.CreateCell(10);
            cell.SetCellValue("TELEFONO");

            cell = row.CreateCell(11);
            cell.SetCellValue("DESCRIPCION");

            cell = row.CreateCell(12);
            cell.SetCellValue("CUIT");

            cell = row.CreateCell(13);
            cell.SetCellValue("PRODUCTOR");

            foreach(NPOI.SS.UserModel.ICell column in row.Cells)
                column.CellStyle = styles;
        }

        private void setColumnData(IRow row,Insured insured,ICellStyle companyStyle) {
            NPOI.SS.UserModel.ICell cell;
            string born, address, phones, folder, namesAndPolicy;

            born = insured.Born.ToString("dd/MM/yyyy hh:mm:ss").Split(" ")[0];
            address = insured.AddressNavigation.Street + " " +
                      insured.AddressNavigation.Number;
            phones = "";
            folder = insured.Folder != 0 ? insured.Folder.ToString() : "SIN CARPETA";
            namesAndPolicy = insured.Firstname + " " + insured.Lastname;

            //completing address
            if(insured.AddressNavigation.Floor != null)
                address += " P " + insured.AddressNavigation.Floor.ToString();
            if(insured.AddressNavigation.Departament != null)
                address += " DTO " + insured.AddressNavigation.Departament;
            
            //completing phones
            for(int i = 0;i < insured.Phones.Count;++i) {
                Phone phone = insured.Phones.ElementAt(i);
                phones += phone.Number;
                
                if(phone.Description != null)
                    phones += " (" + phone.Description + ")";
                
                if((i + 1) < insured.Phones.Count)
                    phones += "/";
            }

            //completing policy
            if(insured.InsurancePolicy != null)
                namesAndPolicy += " (" + insured.InsurancePolicy + ") ";

            //completing company 
            if(insured.Company == 1) {
                //company is cooperación, so the background
                //will be align to the producer
                switch(insured.Producer) {
                    case 1:
                        companyStyle.FillForegroundColor =
                    NPOI.HSSF.Util.HSSFColor.Pink.Index;
                        break;
                    case 2:
                        companyStyle.FillForegroundColor =
                    NPOI.HSSF.Util.HSSFColor.Yellow.Index;
                        break;
                    case 3:
                        companyStyle.FillForegroundColor =
                    NPOI.HSSF.Util.HSSFColor.Green.Index;
                        break;
                    case 4:
                        companyStyle.FillForegroundColor =
                    NPOI.HSSF.Util.HSSFColor.Orange.Index;
                        break;
                    case 5:
                        companyStyle.FillForegroundColor =
                    NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                        break;
                }
            } else {
                //company is federación, so the background
                //will be white
                companyStyle.FillForegroundColor =
                    NPOI.HSSF.Util.HSSFColor.White.Index;
            }

            companyStyle.FillPattern = FillPattern.SolidForeground;
            companyStyle.BorderBottom = BorderStyle.Thin;
            companyStyle.BorderLeft = BorderStyle.Thin;
            companyStyle.BorderRight = BorderStyle.Thin;

            //setting values
            cell = row.CreateCell(0);
            cell.SetCellValue(insured.License);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(1);
            cell.SetCellValue(folder);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(2);
            cell.SetCellValue(insured.Life);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(3);
            cell.SetCellValue(namesAndPolicy);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(4);
            cell.SetCellValue(born);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(5);
            cell.SetCellValue(address);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(6);
            cell.SetCellValue(insured.Status);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(7);
            cell.SetCellValue(insured.PaymentExpiration);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(8);
            cell.SetCellValue(insured.AddressNavigation.City);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(9);
            cell.SetCellValue("DNI " + insured.Dni);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(10);
            cell.SetCellValue(phones);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(11);
            cell.SetCellValue(insured.Description);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(12);
            cell.SetCellValue(insured.Cuit);
            cell.CellStyle = companyStyle;

            cell = row.CreateCell(13);
            cell.SetCellValue(insured.ProducerNavigation.Firstname);
            cell.CellStyle = companyStyle;
        }
    }
}
