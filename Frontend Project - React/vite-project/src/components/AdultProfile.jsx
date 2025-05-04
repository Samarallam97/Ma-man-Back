import React, { useState } from 'react';
import adultimage from '../assets/0eg0aG0.jpg';
import './adult.css';
import { Helmet } from 'react-helmet-async';
import { useNavigate, Link } from 'react-router-dom';


const AdultProfile = () => {const [name, setName] = useState("");
    const [lname, setLname] = useState("");
    const [email, setEmail] = useState("");
    const [phone, setPhone] = useState("");
    const [address, setAddress] = useState("");
    const [country, setCountry] = useState("");
    const [bank, setBank] = useState("");
    const [accountnumber, setBanknum] = useState("");
    const navigate = useNavigate();

  const handleSubmit = () => {
    navigate('/Content_title', {
      state: { name: name } // أرسل البيانات هنا
    });
  };
  return (
    <div>
        <Helmet><title>My web page</title></Helmet>
       <div className="container rounded #682773">
        <div className="row">
            <div className="col-md-4 border-right">
                <div className="d-flex flex-column align-items-center text-center p-3 py-5">
                    <img className="rounded-circle mt-5" src={adultimage} width="90" />
                    <span className="font-weight-bold">John Doe</span>
                    <span className="text-black-50">john_doe12@bbb.com</span>
                    <span>United States</span>
                </div>
            </div>
            <div className="col-md-8">
                <div className="p-3 py-5">
                    <div className="d-flex justify-content-between align-items-center mb-3">
                        <div className="d-flex flex-row align-items-center back">
                            <i className="fa fa-long-arrow-left mr-1 mb-1"></i>
                            <Link to ="/">
                            <h6 className=" btn btn-secondary">Back to home</h6>
                            </Link>
                        </div>
                        <h6 className=" btn btn-secondary text-right">Edit Profile</h6>
                    </div>
                    <div className="row mt-2">
                        <div className="col-md-6">
                            <input type="text"
                             className="form-control" 
                             placeholder="first name"
                              value={name}  onChange={(e) => setName(e.target.value)}/>

                        </div>
                        <div className="col-md-6">
                            <input type="text"
                             className="form-control"
                              value={lname}
                               placeholder="Last Name" 
                               onChange={(e) => setLname(e.target.value)}/>
                        </div>
                    </div>
                    <div className="row mt-3">
                        <div className="col-md-6">
                            <input type="text" 
                            className="form-control" 
                            placeholder="Email" 
                            value={email} 
                            onChange={(e)=> setEmail(e.target.value)}/>
                        </div>
                        <div className="col-md-6">
                            <input type="text"
                             className="form-control" 
                             value={phone} 
                             placeholder="Phone number"
                             onChange={(e) => setPhone(e.target.value)}/>
                        </div>
                    </div>
                    <div className="row mt-3">
                        <div className="col-md-6">
                            <input type="text"
                             className="form-control"
                              placeholder="address"
                               value={address}
                               onChange={(e) => setAddress(e.target.value)}/>
                        </div>
                        <div className="col-md-6">
                            <input type="text" 
                            className="form-control"
                             value={country} 
                             placeholder="Country"
                             onChange={(e) =>setCountry(e.target.value)}/>
                        </div>
                    </div>
                    <div className="row mt-3">
                        <div className="col-md-6">
                            <input type="text"
                             className="form-control" 
                             placeholder="Bank Name" 
                             value={bank}
                             onChange={(e)=>setBank(e.target.value)}/>
                            </div>
                        <div className="col-md-6">
                            <input type="text"
                             className="form-control"
                              value={accountnumber}
                               placeholder="Account Number"
                               onChange={(e) =>setBanknum(e.target.value)}/>
                        </div>
                    </div>
                    <div className="mt-5 text-right">
                       
                        <button className="btn btn-primary profile-button" 
                        type="button"
                        onClick={handleSubmit}>
                            Submit</button>
                         
                    </div>
                </div>
            </div>
        </div>
    </div>  
    </div>
);
}

export default AdultProfile