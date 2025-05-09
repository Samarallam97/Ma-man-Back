import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './components/Home';
import AdultProfile from './components/AdultProfile.jsx';
import Content_title from './components/Content_title.jsx';
import Addnewkid from './components/Addnewkid.jsx';
import Error404 from './components/Error404.jsx';
import ParentProfile from './components/ParentProfile.jsx';
import { HelmetProvider } from 'react-helmet-async';
import Diary from './components/Diary.jsx';
import To_do_list from './components/To_do_list.jsx';
import Achievements from './components/Achievements.jsx';
import Reminder from './components/Reminder.jsx';
import Tasks from './components/Tasks.jsx';
import Notifications from './components/Notifications.jsx';
import Friends from './components/Friends.jsx';``
import Register from './components/Register.jsx'; 
import Login from './components/Login.jsx'; 
import AboutParent from './components/AboutParent.jsx';
function App() {
  return (<>
  <HelmetProvider>
    <Router>
      <Routes>
        {/* <Route path="/"  element={<ParentProfile />} /> */}
        <Route path="/add-new-kid"  element={<Addnewkid />} />
        <Route path="/error-404"  element={<Error404 />} />
        <Route path="/"  element={<Home />} />
        <Route path="/register"  element={<Register />} />
        <Route path="/login"  element={<Login />} />
        <Route path="/parentprofile" element={<ParentProfile/>} />
        <Route path="/Content_title" element={<Content_title/> } />
        <Route path="/AdultProfile" element={<AdultProfile/> } />
        <Route path="/diary" element={<Diary/> } />
        <Route path="/to-do-list" element={<To_do_list/> } />
        <Route path="/reminder" element={<Reminder/> } />
        <Route path="/achievements" element={<Achievements/> } />
        <Route path="/tasks" element={<Tasks/> } />
        <Route path="/notifications" element={<Notifications/> } />
        <Route path="/friends" element={<Friends/> } />
        <Route path="/aboutparent" element={<AboutParent/> } />

      </Routes>
    </Router> 
   
  </HelmetProvider>





</>
);
}

export default App;

