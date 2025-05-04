import React, { useState } from 'react';
import './addnewkid.css';
import signupimg from '../assets/signup-bg.jpg';
import { Helmet } from 'react-helmet-async';
const Addnewkid = () => {
    const [gender, setGender] = useState("male");

    return (<>
        <div className="main">
        <Helmet>
            <title>Add_new_kid</title>
            </Helmet>
            <section className="signup" style={{ backgroundImage: `url(${signupimg})` }}>
                <div className="container">
                    <div className="signup-content">
                        <form method="POST" id="signup-form" className="signup-form">
                            <div className="form-row">
                                <div className="form-group">
                                    <label htmlFor="first_name">First name</label>
                                    <input type="text" className="form-input" name="first_name" id="first_name" />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="last_name">Last name</label>
                                    <input type="text" className="form-input" name="last_name" id="last_name" />
                                </div>
                            </div>
                            <div className="form-row">
                                <div className="form-group form-icon">
                                    <label htmlFor="birth_date">Birth date</label>
                                    <input type="text" className="form-input" name="birth_date" id="birth_date" placeholder="MM-DD-YYYY" />
                                </div>
                                <div className="form-radio">
                                    <label htmlFor="gender">Gender</label>
                                    <div className="form-flex">
                                        <input type="radio" name="gender" value="male" id="male" checked={gender === "male"} onChange={() => setGender("male")} />
                                        <label htmlFor="male">Male</label>
                                        <input type="radio" name="gender" value="female" id="female" checked={gender === "female"} onChange={() => setGender("female")} />
                                        <label htmlFor="female">Female</label>
                                    </div>
                                </div>
                            </div>

                            <div className="form-text">
                                
                                <div className="add_info">
                                    <div className="form-row">
                                        <div className="form-group">
                                            <label htmlFor="time_limit">Time Limit</label>
                                            <input type="number" className="form-input" name="time_limit" id="time_limit" />
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="country">Language</label>
                                            <div className="select-list">
                                                <select name="country" id="country" required>
                                                    <option value="US">Arabic</option>
                                                    <option value="UK">English</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="form-group">
                                <input type="submit" name="submit" id="submit" className="form-submit" value="Submit" />
                            </div>
                        </form>
                    </div>
                </div>
            </section>
        </div>
        </>    
    );
};

export default Addnewkid;
