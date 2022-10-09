import { combineReducers } from '@reduxjs/toolkit';
import students from './studentsSlice';
import schools from './schoolsSlice';
import student from './studentSlice';

const reducer = combineReducers({
  students,
  schools,
  student,
});

export default reducer;
