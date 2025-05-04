import React from 'react';
import { Link } from 'react-router-dom';
const Reminder = () => {
  return (
    <div>
        <div className="container mt-5">
    <h1>Reminders</h1>
    <p>Manage your important reminders.</p>
    <input type="text" className="form-control mb-2" placeholder="Reminder text"/>
    <input type="date" className="form-control mb-2"/>
    <button className="btn btn-primary">Set Reminder</button>
    <ul className="list-group mt-4">
      <li className="list-group-item">Doctor Appointment – 2025-05-01</li>
    </ul>
    <Link to="/content_title" className="btn btn-secondary mt-4">Back</Link>
  </div>
    </div>
  )
}

export default Reminder;