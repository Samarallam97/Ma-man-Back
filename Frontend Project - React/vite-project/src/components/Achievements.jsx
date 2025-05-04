import React from 'react';
import { Link } from 'react-router-dom';
const Achievements = () => {
  return (
    <div>
        <div className="container mt-5">
    <h1>Achievements</h1>
    <p>Track your milestones and achievements.</p>
    <ul className="list-group">
      <li className="list-group-item">Finished 5 tasks this week!</li>
      <li className="list-group-item">Wrote in diary 7 days in a row!</li>
    </ul>
    <Link to="/content_title" className="btn btn-secondary mt-4">Back</Link>
  </div>
    </div>
  )
}

export default Achievements