import { combineReducers } from '@reduxjs/toolkit';
import schoolTypes from './schoolTypesSlice';
import schoolType from './schoolTypeSlice';
import school from './schoolSlice';
import schools from './schoolsSlice';
import staffType from './staffTypeSlice';
import staffTypes from './staffTypesSlice';

const reducer = combineReducers({
  schoolTypes,
  schoolType,
  schools,
  school,
  staffTypes,
  staffType,
});

export default reducer;
