import React from 'react'

const Friends = () => {
  return (
    <div className="container py-5">
      <h1 className="text-center mb-5">👥 Friends List</h1>

      <div className="row g-4">
        {/* Friend 1 */}
        <div className="col-md-4">
          <div className="card h-100">
            <img
              src="https://randomuser.me/api/portraits/men/32.jpg"
              className="card-img-top"
              alt="Friend 1"
            />
            <div className="card-body">
              <h5 className="card-title">Ahmed Ali</h5>
              <p className="card-text">Loves hiking and photography 🌄📸</p>
              <div className="card-buttons d-flex justify-content-between">
                <a href="friend1.html" className="btn btn-secondary w-50 me-1">View</a>
                <button className="btn btn-outline-danger w-50">Hide</button>
              </div>
            </div>
          </div>
        </div>

        {/* Friend 2 */}
        <div className="col-md-4">
          <div className="card h-100">
            <img
              src="https://randomuser.me/api/portraits/women/45.jpg"
              className="card-img-top"
              alt="Friend 2"
            />
            <div className="card-body">
              <h5 className="card-title">Sara Mohamed</h5>
              <p className="card-text">Enjoys coding and coffee ☕💻</p>
              <div className="card-buttons d-flex justify-content-between">
                <a href="friend2.html" className="btn btn-secondary w-50 me-1">View</a>
                <button className="btn btn-outline-danger w-50">Hide</button>
              </div>
            </div>
          </div>
        </div>

        {/* Friend 3 */}
        <div className="col-md-4">
          <div className="card h-100">
            <img
              src="https://randomuser.me/api/portraits/men/76.jpg"
              className="card-img-top"
              alt="Friend 3"
            />
            <div className="card-body">
              <h5 className="card-title">Youssef Nabil</h5>
              <p className="card-text">Tech lover and bookworm 📚🖥️</p>
              <div className="card-buttons d-flex justify-content-between">
                <a href="friend3.html" className="btn btn-secondary w-50 me-1">View</a>
                <button className="btn btn-outline-danger w-50">Hide</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Friends