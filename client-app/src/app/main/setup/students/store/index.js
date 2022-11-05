import { combineReducers } from '@reduxjs/toolkit';
import students from './studentsSlice';
import schools from './schoolsSlice';
import student from './studentSlice';
import documentTypes from './documentTypesSlice';

const reducer = combineReducers({
  students,
  schools,
  student,
  documentTypes,
});

export default reducer;
