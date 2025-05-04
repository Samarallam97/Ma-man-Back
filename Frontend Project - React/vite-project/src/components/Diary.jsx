import React from 'react';
import { Link } from 'react-router-dom';
const Diary = () => {
  return (
    <div>
        <div className="container mt-5">
    <h1>My Diary</h1>
    <p>Write and review your daily entries here.</p>
    <textarea className="form-control" rows="8" placeholder="Start writing..."></textarea>
    <button className="btn btn-primary mt-3">Save</button>
    <Link to="/Content_title" className="btn btn-secondary mt-3 ms-2">Back</Link>
  </div>
    </div>
  )
}

export default Diary;