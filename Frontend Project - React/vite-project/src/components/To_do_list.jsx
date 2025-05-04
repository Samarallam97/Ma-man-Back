import React from 'react';
import { Link } from 'react-router-dom';

const To_do_list = () => {
  return (
    <div>
      <div className="container mt-5">
    <h1>To-Do List</h1>
    <p>Add and track your tasks.</p>
    <input type="text" className="form-control mb-2" placeholder="Enter new task"/>
    <button className="btn btn-primary">Add Task</button>
    <ul className="list-group mt-4">
      <li className="list-group-item">Example Task 1</li>
      <li className="list-group-item">Example Task 2</li>
    </ul>
    <Link to="/content_title" className="btn btn-secondary mt-4">Back</Link>
  </div>
    </div>
  )
}

export default To_do_list;