import React, { useState } from 'react';
import './content_title.css';
import adultimage from '../assets/0eg0aG0.jpg';
import { useLocation  } from 'react-router-dom';
import { Link } from 'react-router-dom';

const Content_title = () => {
    const location = useLocation();
    const name = location.state?.name || ""; // استقبل الاسم من الصفحة السابقة
  
  return (
    <div className='content-title'>
        <nav className="navbar navbar-expand-lg navbar-dark">
    <div className="container-fluid">
        <a className="navbar-brand" href="home.html">Your Content</a>
        <form className="d-flex mx-auto" role="search">
            <input className="form-control me-2" type="search" placeholder="Search" aria-label="Search" style={{width: '400px'}}/>
            <button className="btn btn-outline-light" type="submit"><i className="bi bi-search"></i></button>
        </form>
        <div className="d-flex align-items-center gap-3">
            <Link to='/friends' className="nav-icon text-white"><i className="bi bi-people-fill"></i></Link>
            <a href="profile.html" className="nav-icon text-white"><i className="bi bi-person-fill"></i></a>
            <a href="/" className="nav-icon text-white"><i className="bi bi-house-fill"></i></a>
            <a href="#" className="nav-icon text-white"><i className="bi bi-moon-fill"></i></a>
            <Link to='/notifications' className="nav-icon text-white"><i className="bi bi-bell-fill"></i></Link>
        </div>
    </div>
</nav>
{/* <!-- Main Content --> */}
    <div className="container-fluid mt-4">
        <div className="row">
        {/* <!-- Profile and Cards --> */}
            <div className="col-md-9">
                <div className="d-flex align-items-center mb-4  ">
                    <a href="profile.html">
                        <img src= {adultimage} 
                        alt="Profile" 
                        className="rounded-circle mt-5"/>
                    </a>
                    <a href="profile.html" className="ms-3 text-dark text-decoration-none"><h5>{name} </h5></a>
                </div>
               
            </div>
              {/* <!-- Sidebar --> */}
              <div className="col-md-3">
                <div className="sidebar">
                    <div className="main-buttons">
                        
                        <Link to='/tasks' className="btn"><i className="bi bi-list-check me-2"></i>Tasks</Link>
                    </div>
                    
                   
                    
                    {/* <!-- Leave Button --> */}
                    <div className="leave-container">
                        <Link to="/error-404">
                            <button className="btn btn-primary profile-button" >
                                <i className="bi bi-box-arrow-right me-1"></i>Leave
                            </button>
                            </Link>
                        </div>
                    </div>
            </div>
            <div className="row g-4">
                    <div className="col-md-6">
                        <div className="card-custom">
                        <h5 className="section-title">Diary</h5>
                        <p>Review your daily entries and reflect on your progress.</p>
                        <Link to="/diary"
                        className="btn btn-custom">Open Diary</Link>
                        </div>
                    </div>
                    <div className="col-md-6">
                        <div className="card-custom">
                        <h5 className="section-title">To-Do List</h5>
                        <p>Keep track of your tasks and stay organized.</p>
                        <Link to="/to-do-list" className="btn btn-custom">View tasks</Link>
                        </div>
                    </div>
                    <div className="col-md-6">
                        <div className="card-custom">
                        <h5 className="section-title">Reminders</h5>
                        <p>Set and manage your reminders to never miss important events.</p>
                        <Link to="/reminder" className="btn btn-custom">Manage Reminders</Link>
                        </div>
                    </div>
                    <div className="col-md-6">
                        <div className="card-custom">
                        <h5 className="section-title">Achievements</h5>
                        <p>Set and manage your reminders to never miss important events.</p>
                        <Link to ="/achievements" className="btn btn-custom">Show all</Link>
                        </div>
                    </div>
                    </div>


          
        </div>
    </div>
</div>
  )
}

export default Content_title;