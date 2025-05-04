import React from 'react';
import profileImage from '../assets/profile-img-1.webp';
import { Link } from 'react-router-dom';
import { Helmet  } from 'react-helmet-async';
const ParentProfile = () => {
  return (
  <div>
    {/* <Helmet>
      <title>parent_profile</title>
    </Helmet> */}
    <section className="bg-light py-3 py-md-5 py-xl-8" 
     style={{ width: "100%", minHeight: "100vh" }}>
      <div className="container-fluid" style={{ padding: 0 }}>
        <div className="row gy-4 gy-lg-0">
          <div className="col-12 col-lg-4 col-xl-3">
            <div className="row gy-4">
              <div className="col-12">
                <div className="card widget-card border-light shadow-sm">
                  <div className="card-header text-bg-primary">Welcome, Ethan Leo</div>
                  <div className="card-body">
                    <div className="text-center mb-3">
                      <img src= {profileImage} className="img-fluid rounded-circle" 
                      alt="profile" />
                    </div>
                    <h5 className="text-center mb-1">Ethan Leo</h5>
                    <p className="text-center text-secondary mb-4">Parent</p>
                    <ul className="list-group list-group-flush mb-4">
                      <li className="list-group-item d-flex justify-content-between align-items-center">
                        <h6 className="m-0">Age</h6>
                        <span>45</span>
                      </li>
                      <li className="list-group-item d-flex justify-content-between align-items-center">
                        <h6 className="m-0">Kids</h6>
                        <span>3</span>
                      </li>
                      <li className="list-group-item d-flex justify-content-between align-items-center">
                        <h6 className="m-0">Creation Date</h6>
                        <span>5/5/2025</span>
                      </li>
                    </ul>
                    <div className="d-grid m-0">
                      <Link  to="/add-new-kid">
                        <button  className="btn btn-outline-primary">Add New Kid</button>
                        </Link>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col-12">
                <div className="card widget-card border-light shadow-sm">
                  <div className="card-header text-bg-primary">Kids</div>
                  <div className="card-body">
                    <button className="d-inline-block bg-dark link-light lh-1 p-2 rounded">
                      Sara
                    </button>
                    <button className="d-inline-block bg-dark link-light lh-1 p-2 rounded">
                      Sally
                    </button>
                    <button className="d-inline-block bg-dark link-light lh-1 p-2 rounded">
                      Ali
                    </button>
                  </div>
                </div>
              </div>
              <div className="col-12">
                <div className="card widget-card border-light shadow-sm">
                  <div className="card-header text-bg-primary">About</div>
                  <div className="card-body">
                    <ul className="list-group list-group-flush mb-0">
                      <li className="list-group-item">
                        <h6 className="mb-1">
                          <span className="bi bi-mortarboard-fill me-2"></span>
                          Education
                        </h6>
                        <span>M.S Computer Science</span>
                      </li>
                      <li className="list-group-item">
                        <h6 className="mb-1">
                          <span className="bi bi-geo-alt-fill me-2"></span>
                          Address
                        </h6>
                        <span>Mountain View, California</span>
                      </li>
                      <li className="list-group-item">
                        <h6 className="mb-1">
                          <span className="bi bi-building-fill-gear me-2"></span>
                          Job
                        </h6>
                        <span>Teacher</span>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
              <div className="col-12"><Link to ="/error-404" >
                <button className="btn btn-danger">Log Out</button>
                </Link>
              </div>
            </div>
          </div>
          <div className="col-12 col-lg-8 col-xl-9">
            <div className="card widget-card border-light shadow-sm">
              <div className="card-body p-4">
                <ul className="nav nav-tabs" id="profileTab" role="tablist">
                  <li className="nav-item" role="presentation">
                    <button className="nav-link active"
                     id="overview-tab" 
                     data-bs-toggle="tab"
                      data-bs-target="#overview-tab-pane" 
                      type="button" 
                      role="tab" 
                      aria-controls="overview-tab-pane" 
                      aria-selected="true">Overview</button>
                  </li>
                  <li className="nav-item" role="presentation">
                    <button className="nav-link" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile-tab-pane" type="button" role="tab" aria-controls="profile-tab-pane" aria-selected="false">Profile</button>
                  </li>
                  <li className="nav-item" role="presentation">
                    <button className="nav-link" id="email-tab" data-bs-toggle="tab" data-bs-target="#email-tab-pane" type="button" role="tab" aria-controls="email-tab-pane" aria-selected="false">Notifications</button>
                  </li>
                  <li className="nav-item" role="presentation">
                    <button className="nav-link" id="password-tab" data-bs-toggle="tab" data-bs-target="#password-tab-pane" type="button" role="tab" aria-controls="password-tab-pane" aria-selected="false">Passwords</button>
                  </li>
                  <li className="nav-item" role="presentation">
                    <button className="nav-link" id="socity-tab" data-bs-toggle="tab" data-bs-target="#socity-tab-pane" type="button" role="tab" aria-controls="socity-tab-pane" aria-selected="false">Society</button>
                  </li>
                  <li className="nav-item" role="presentation">
                    <button className="nav-link" id="kids-tab" data-bs-toggle="tab" data-bs-target="#kids-tab-pane" type="button" role="tab" aria-controls="kids-tab-pane" aria-selected="false">Kids</button>
                  </li>
                </ul>
                <div className="tab-content pt-4" id="profileTabContent">
                 
                  <div className="tab-pane fade show active" id="overview-tab-pane" role="tabpanel" aria-labelledby="overview-tab" tabIndex={0}>
                  <h5 className="mb-3">About</h5>
                  <p className="lead mb-3">Ethan Leo is a seasoned and results-driven Project Manager who brings experience and expertise to project management. With a proven track record of successfully delivering complex projects on time and within budget, Ethan Leo is the go-to professional htmlFor organizations seeking efficient and effective project leadership.</p>
                  <div className="">
                    <button className="btn btn-outline-primary mb-3" type="button">Edit</button>
                  </div>
                  <h5 className="mb-3">About Sara</h5>
                  <p className="lead mb-3">Ethan Leo is a seasoned and results-driven Project Manager who brings experience and expertise to project management. With a proven track record of successfully delivering complex projects on time and within budget, Ethan Leo is the go-to professional htmlFor organizations seeking efficient and effective project leadership.</p>
                  <div className="">
                    <button className="btn btn-outline-primary mb-3" type="button">Edit</button>
                  </div>
                  <h5 className="mb-3">About Sally</h5>
                  <p className="lead mb-3">Ethan Leo is a seasoned and results-driven Project Manager who brings experience and expertise to project management. With a proven track record of successfully delivering complex projects on time and within budget, Ethan Leo is the go-to professional htmlFor organizations seeking efficient and effective project leadership.</p>
                 <div className="">
                    <button className="btn btn-outline-primary mb-3" type="button">Edit</button>
                  </div>
                  <h5 className="mb-3">About Ali</h5>
                  <p className="lead mb-3">Ethan Leo is a seasoned and results-driven Project Manager who brings experience and expertise to project management. With a proven track record of successfully delivering complex projects on time and within budget, Ethan Leo is the go-to professional htmlFor organizations seeking efficient and effective project leadership.</p>
                  <div className="">
                    <button className="btn btn-outline-primary mb-3" type="button">Edit</button>
                  </div>
                </div>
              </div>
            </div>            
          </div>
        </div>
      </div>
    </div>
      </section>

   

      
  </div>
            
  );
}

export default ParentProfile