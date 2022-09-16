import { combineReducers } from '@reduxjs/toolkit';
import schoolTypes from './schoolTypesSlice';
import schoolType from './schoolTypeSlice';
import school from './schoolSlice';
import schools from './schoolsSlice';

const reducer = combineReducers({
  schoolTypes,
  schoolType,
  schools,
  school,
});

export default reducer;
