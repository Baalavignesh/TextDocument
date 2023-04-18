import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import './custom.css';
import HomePage from './Pages/Home/HomePage';
import EditPage from './Pages/Edit/EditPage';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Routes>
        <Route path='/:username' element={<HomePage />} />
        <Route path='/:username/:filename' element={<EditPage />} />
        <Route path='*' element={<HomePage />} />
      </Routes>
    );
  }
}
