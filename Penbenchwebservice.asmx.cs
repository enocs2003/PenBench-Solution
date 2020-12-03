using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PENSION.Controllers;
using PENSION.Models;
using System.Web.Hosting;

namespace Pension
{   
    /// <summary>
    /// Summary description for Penbenchwebservice
    /// </summary>
    [WebService(Namespace = "http://enovizioninventltd.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Penbenchwebservice : System.Web.Services.WebService
    {
        DB_string_PEN db_con = new DB_string_PEN();
        string constr;
        string path = HostingEnvironment.MapPath("~/Logs");
        select_all_PEN sel;
        insert_PEN ins;
        delete_PEN del;
        update_PEN upd;
        utility util;
        miscellaneous_PEN misc;
        System.Data.DataTable dtab;

        [WebMethod]
        public Boolean VerificationSync(string pid, string surname, string other_names, string state_of_origin, string geo_political_zone, string residential_address, string permanent_address, string email, string home_phone, string mobile_phone, string alt_mobile_phone, string bank, string account_no, string account_type, string bvn, string sort_code, string nok_name_1, string nok_relationship_1, string nok_email_1, string nok_mobile_1, string nok_alt_mobile_1, string nok_address_1, string nok_name_2, string nok_relationship_2, string nok_email_2, string nok_mobile_2, string nok_alt_mobile_2, string nok_address_2, object photo, object thumb_print, object thumb_print_2, object date_of_verification, string uid)
        {
            constr=db_con.Get_ConnectionString();
            ins = new insert_PEN(constr, path);
            misc = new miscellaneous_PEN(constr, path);
            int foundindex = misc.existindex(surname, other_names, state_of_origin, mobile_phone, pid);
            Boolean reply=false;
            if (foundindex < 1)
            {
                reply = ins.insert_TBL_VERIFICATION_TEMP_FXN(pid, surname, other_names, state_of_origin, geo_political_zone, residential_address, permanent_address, email, home_phone, mobile_phone, alt_mobile_phone, bank, account_no, account_type, bvn, sort_code, nok_name_1, nok_relationship_1, nok_email_1, nok_mobile_1, nok_alt_mobile_1, nok_address_1, nok_name_2, nok_relationship_2, nok_email_2, nok_mobile_2, nok_alt_mobile_2, nok_address_2, photo, thumb_print, thumb_print_2, date_of_verification, uid);
            }
            else
            {
                upd = new update_PEN(constr, path);
                reply = upd.update_TBL_VERIFICATION_TEMP_FXN(pid, surname, other_names, state_of_origin, geo_political_zone, residential_address, permanent_address, email, home_phone, mobile_phone, alt_mobile_phone, bank, account_no, account_type, bvn, sort_code, nok_name_1, nok_relationship_1, nok_email_1, nok_mobile_1, nok_alt_mobile_1, nok_address_1, nok_name_2, nok_relationship_2, nok_email_2, nok_mobile_2, nok_alt_mobile_2, nok_address_2, photo, thumb_print, thumb_print_2, date_of_verification, uid, foundindex);
            }
            return reply;
        }

        [WebMethod]
        public Boolean ComplaintsSync(object date_of_complain, string pid, string surname, string other_names, object dob, string state_of_origin, object dofa, object dor, string omitted_monthly_pen_from, string omitted_monthly_pen_to, object non_enrollment, object non_payment_of_gratuity, object short_payment, object short_payment_gratuity, string any_other, string current_contact_address, string mobile_numbers, string bank_name, string bvn, string account_no, string account_type, string nok_name, string relationship, string mobile_number, string address, object photo, object thumb_print, object new_flag, object upd_flag, object del_flag, string uid)
        {
            constr = db_con.Get_ConnectionString();
            ins = new insert_PEN(constr, path);
            util = new utility(path);
            object mfrom = util.XmlValueConvert(omitted_monthly_pen_from);
            object mto = util.XmlValueConvert(omitted_monthly_pen_to);
            misc = new miscellaneous_PEN(constr,path);
            int compindex = misc.ComplaintExistIndex(surname, other_names, state_of_origin, mobile_numbers, pid);
            Boolean reply = false;
            if (compindex < 1)
            {
                reply = ins.insert_TBL_COMPLAINTS_TEMP_FXN(date_of_complain, pid, surname, other_names, dob, state_of_origin, dofa, dor, mfrom, mto, non_enrollment, non_payment_of_gratuity, short_payment, short_payment_gratuity, any_other, current_contact_address, mobile_numbers, bank_name, bvn, account_no, account_type, nok_name, relationship, mobile_number, address, photo, thumb_print, new_flag, upd_flag, del_flag, uid);
            }
            else
            {
                upd = new update_PEN(constr, path);
                reply = upd.update_TBL_COMPLAINTS_TEMP_FXN(date_of_complain, pid, surname, other_names, dob, state_of_origin, dofa, dor, mfrom, mto, non_enrollment, non_payment_of_gratuity, short_payment, short_payment_gratuity, any_other, current_contact_address, mobile_numbers, bank_name, bvn, account_no, account_type, nok_name, relationship, mobile_number, address, photo, thumb_print, new_flag, upd_flag, del_flag, uid,compindex);
            }
            return reply;
        }

        [WebMethod]
        public List<string[]> ReturnUsers()
        {
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            util = new utility(path);
            List<GetLogin> lstlogin = new List<GetLogin>();
            try
            {
                dtab = sel.select_TBL_LOGIN_FXN("SYSTEM","","","");
                foreach (DataRow drow in dtab.Rows)
                {
                    lstlogin.Add(new GetLogin
                    {
                        int_index = Convert.ToInt32(drow["int_index"].ToString()),
                        username = drow["username"].ToString(),
                        password = drow["password"].ToString(),
                        user_group = drow["user_group"].ToString(),
                        active = drow["active"].ToString(),
                        retire_from = util.SetDateString(drow["retire_from"].ToString()),
                        retire_to = util.SetDateString(drow["retire_to"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnUsers");
            }
            List<string[]> users = new List<string[]>();
            string[] user;
            foreach (var login in lstlogin)
            {
                user = new string[] { "", "", "", "", "", "", "" };
                user[0] = login.int_index.ToString();
                user[1] = login.username;
                user[2] = login.password;
                user[3] = login.user_group;
                user[4] = login.active.ToString();
                user[5] = login.retire_from.ToString();
                user[6] = login.retire_to.ToString();

                users.Add(user);
            }
            return users;
        }

        [WebMethod]
        public List<string[]> ReturnBanks()
        {
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            util = new utility(path);
            List<GetBanks> lstbanks = new List<GetBanks>();
            try
            {
                dtab = sel.select_TBL_BANKS_FXN("SYSTEM", "", "", "");
                foreach (DataRow drow in dtab.Rows)
                {
                    lstbanks.Add(new GetBanks
                    {
                        int_index = Convert.ToInt32(drow["int_index"].ToString()),
                        bank_name = drow["bank_name"].ToString(),
                        bank_short_name = drow["bank_short_name"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnBanks");
            }
            List<string[]> banks = new List<string[]>();
            string[] bank;
            foreach (var bankdet in lstbanks)
            {
                bank = new string[] { "", "", "" };
                bank[0] = bankdet.int_index.ToString();
                bank[1] = bankdet.bank_name;
                bank[2] = bankdet.bank_short_name;

                banks.Add(bank);
            }
            return banks;
        }

        [WebMethod]
        public List<string[]> ReturnStates()
        {
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            util = new utility(path);
            List<GetState> lststates = new List<GetState>();
            try
            {
                dtab = sel.select_TBL_STATE_FXN("SYSTEM", "Country", "", "Nigeria");
                foreach (DataRow drow in dtab.Rows)
                {
                    lststates.Add(new GetState
                    {
                        int_index = Convert.ToInt32(drow["int_index"].ToString()),
                        country = drow["country"].ToString(),
                        state = drow["state"].ToString(),
                        state_code = drow["state_code"].ToString(),
                        geo_pol_zone = drow["geo_pol_zone"].ToString(),
                        console = drow["console"].ToString(),
                        zip_code = drow["zip_code"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnStates");
            }
            List<string[]> states = new List<string[]>();
            string[] state;
            foreach (var statedet in lststates)
            {
                state = new string[] { "", "", "", "", "", "", "" };
                state[0] = statedet.int_index.ToString();
                state[1] = statedet.country;
                state[2] = statedet.state;
                state[3] = statedet.state_code;
                state[4] = statedet.geo_pol_zone;
                state[5] = statedet.console;
                state[6] = statedet.zip_code;
                states.Add(state);
            }
            return states;
        }

        [WebMethod]
        public List<string[]> ReturnLGA()
        {
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            util = new utility(path);
            List<GetLga> lstlgas = new List<GetLga>();
            try
            {
                dtab = sel.select_TBL_LGA_FXN("SYSTEM", "Country", "", "Nigeria");
                foreach (DataRow drow in dtab.Rows)
                {
                    lstlgas.Add(new GetLga
                    {
                        int_index = Convert.ToInt32(drow["int_index"].ToString()),
                        country = drow["country"].ToString(),
                        state = drow["state"].ToString(),
                        lga = drow["lga"].ToString(),
                        code = drow["code"].ToString(),
                        senatorial_zone = drow["senatorial_zone"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnLGA");
            }
            List<string[]> lgas = new List<string[]>();
            string[] lga;
            foreach (var lgadet in lstlgas)
            {
                lga = new string[] { "", "", "", "", "", ""};
                lga[0] = lgadet.int_index.ToString();
                lga[1] = lgadet.country;
                lga[2] = lgadet.state;
                lga[3] = lgadet.lga;
                lga[4] = lgadet.code;
                lga[5] = lgadet.senatorial_zone;
                lgas.Add(lga);
            }
            return lgas;
        }

        [WebMethod]
        public List<string[]> ReturnRelationship()
        {
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            util = new utility(path);
            List<GetRelationship> lstrelations = new List<GetRelationship>();
            try
            {
                dtab = sel.select_TBL_RELATIONSHIP_FXN("SYSTEM", "", "", "");
                foreach (DataRow drow in dtab.Rows)
                {
                    lstrelations.Add(new GetRelationship
                    {
                        int_index = Convert.ToInt32(drow["int_index"].ToString()),
                        row_code = drow["row_code"].ToString(),
                        relationship = drow["relationship"].ToString(),
                        description = drow["description"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnRelationship");
            }
            List<string[]> relationships = new List<string[]>();
            string[] relation;
            foreach (var relationdet in lstrelations)
            {
                relation = new string[] { "", "", "", "" };
                relation[0] = relationdet.int_index.ToString();
                relation[1] = relationdet.row_code;
                relation[2] = relationdet.relationship;
                relation[3] = relationdet.description;
                relationships.Add(relation);
            }
            return relationships;
        }

        [WebMethod]
        public List<string[]> ReturnPensionerBasic(string geopol)
        {
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            util = new utility(path);
            List<string[]> pensioners = new List<string[]>();
            string[] pensioner;
            try
            {
                //dtab = sel.select_TBL_PENSIONER_BASIC_FXN("SYSTEM", "geo_po_zone", "", geopol);
                dtab = sel.Custom_Pensioner_Details(geopol);
                foreach (DataRow dr in dtab.Rows)
                {
                    //lstpensioner.Add(new GetPensionerbasic
                    //{
                    //    int_index = Convert.ToInt32(drow["int_index"].ToString()),
                    //    title = drow["title"].ToString(),
                    //    psid = drow["psid"].ToString(),
                    //    pin = drow["pin"].ToString(),
                    //    pensioner_category = drow["pensioner_category"].ToString(),
                    //    surname = drow["surname"].ToString(),
                    //    other_names = drow["other_names"].ToString(),
                    //    dob = util.SetDateString(drow["dob"].ToString()),
                    //    sex = drow["sex"].ToString(),
                    //    state_of_origin = drow["state_of_origin"].ToString(),
                    //    lga = drow["lga"].ToString(),
                    //    geo_po_zone = drow["geo_po_zone"].ToString(),
                    //    tribe = drow["tribe"].ToString(),
                    //    marital_status = drow["marital_status"].ToString()
                    //});

                    pensioner = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                    pensioner[0] = dr["int_index"].ToString();
                    pensioner[1] = dr["title"].ToString();
                    pensioner[2] = dr["psid"].ToString();
                    pensioner[3] = dr["pin"].ToString();
                    pensioner[4] = dr["pensioner_category"].ToString();
                    pensioner[5] = dr["surname"].ToString();
                    pensioner[6] = dr["other_names"].ToString();
                    pensioner[7] = util.SetDateString(dr["dob"].ToString());
                    pensioner[8] = dr["sex"].ToString();
                    pensioner[9] = dr["state_of_origin"].ToString();
                    pensioner[10] = dr["lga"].ToString();
                    pensioner[11] = dr["geo_po_zone"].ToString();
                    pensioner[12] = dr["tribe"].ToString();
                    pensioner[13] = dr["marital_status"].ToString();
                    pensioner[14] = dr["email"].ToString();     //Contacts
                    pensioner[15] = dr["home_phone"].ToString();
                    pensioner[16] = dr["mobile_phone_no"].ToString();
                    pensioner[17] = dr["alt_mobile_no"].ToString();
                    pensioner[18] = dr["residential_address"].ToString();
                    pensioner[19] = dr["permanent_address"].ToString();
                    pensioner[20] = dr["bank"].ToString();      //Bank
                    pensioner[21] = dr["account_no"].ToString();
                    pensioner[22] = dr["bvn"].ToString();
                    pensioner[23] = dr["account_type"].ToString();
                    pensioner[24] = dr["sort_code"].ToString();
                    pensioners.Add(pensioner);
                }                

            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnPensionerBasic");
            }
            return pensioners;
        }

        [WebMethod]
        public List<GetBiometric> ReturnImage(string geopol)
        {
            util = new utility(path);
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            List<GetBiometric> lstbiometric = new List<GetBiometric>();
            try
            {
                dtab = sel.Custom_Biometric(geopol);
                foreach (DataRow drow in dtab.Rows)
                {
                    lstbiometric.Add(new GetBiometric
                    {
                        int_index = Convert.ToInt32(drow["int_index"].ToString()),
                        pid = drow["pid"].ToString(),
                        image_type = drow["image_type"].ToString(),
                        img_data=drow["img_data"]
                    });
                }
            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnBiometric");
            }
            List<string[]> biometric = new List<string[]>();
            string[] bioarr;
            foreach (var bio in lstbiometric)
            {
                bioarr = new string[] { "", "", "", "" };
                bioarr[0] = bio.int_index.ToString();
                bioarr[1] = bio.pid;
                bioarr[2] = bio.image_type;
                bioarr[3] = bio.img_data.ToString();

                biometric.Add(bioarr);
            }
            return lstbiometric;
        }

        [WebMethod]
        public List<string[]> ReturnNOK(string geopol)
        {
            constr = db_con.Get_ConnectionString();
            sel = new select_all_PEN(constr, path);
            util = new utility(path);
            List<GetNok> lstnok = new List<GetNok>();
            try
            {
                dtab = sel.Custom_Get_NOK(geopol);
                foreach (DataRow drow in dtab.Rows)
                {
                    lstnok.Add(new GetNok
                    {
                        int_index = Convert.ToInt32(drow["int_index"].ToString()),
                        pid=drow["pid"].ToString(),
                        nok_name = drow["nok_name"].ToString(),
                        relationship = drow["relationship"].ToString(),
                        mobile_no = drow["mobile_no"].ToString(),
                        alt_mobile_no = drow["alt_mobile_no"].ToString(),
                        email = drow["email"].ToString(),
                        address = drow["address"].ToString(),
                        benficiary = drow["beneficiary"].ToString(),
                        bank = drow["bank"].ToString(),
                        account_type = drow["account_type"].ToString(),
                        account_no = drow["account_no"].ToString(),
                        bvn = drow["bvn"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                util.ErrorLog(ex.Message, "Webservice", "ReturnBanks");
            }
            List<string[]> noks = new List<string[]>();
            string[] nokarr;
            foreach (var nok in lstnok)
            {
                nokarr = new string[] { "", "", "", "", "", "", "", "", "", "", "", "","" };
                nokarr[0] = nok.int_index.ToString();
                nokarr[1]= nok.pid;
                nokarr[2]=nok.nok_name;
                nokarr[3]=nok.relationship;
                nokarr[4]=nok.mobile_no;
                nokarr[5]=nok.alt_mobile_no;
                nokarr[6]=nok.email;
                nokarr[7]=nok.address;
                nokarr[8]=nok.benficiary.ToString();
                nokarr[9]=nok.bank;
                nokarr[10]=nok.account_type;
                nokarr[11]=nok.account_no;
                nokarr[12]=nok.bvn;

                noks.Add(nokarr);
            }
            return noks;
        }

    }
}
