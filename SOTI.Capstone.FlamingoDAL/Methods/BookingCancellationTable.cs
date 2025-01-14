﻿using SOTI.Capstone.FlamingoDAL.Interfaces;
using SOTI.Capstone.FlamingoDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Capstone.FlamingoDAL.Methods
{
    public class BookingCancellationTable : IBookingCancellation
    {
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataReader reader = null;
        private SqlDataAdapter adapter = null;

        public bool AddCancelEntry(BookingCancellation flight)
        {
            using (con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (cmd = new SqlCommand("usp_CancelFlights", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.Parameters.Add(new SqlParameter("@pnrno", SqlDbType.Int, 32));
                    cmd.Parameters.Add(new SqlParameter("@flightid", SqlDbType.Int, 32));
                    cmd.Parameters.Add(new SqlParameter("@refundstatus", SqlDbType.Char, 1));
                    cmd.Parameters.Add(new SqlParameter("@refundamount", SqlDbType.Int, 32));
                    cmd.Parameters["@pnrno"].Value = flight.PNRNo;
                    cmd.Parameters["@flightid"].Value = flight.FlightId;
                    cmd.Parameters["@refundstatus"].Value = flight.RefundStatus;
                    cmd.Parameters["@refundamount"].Value = flight.RefundAmount;
                    var res = cmd.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
    }
}
