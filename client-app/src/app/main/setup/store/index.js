import { combineReducers } from '@reduxjs/toolkit';
import schoolTypes from './schoolTypesSlice';
import schoolType from './schoolTypeSlice';
import school from './schoolSlice';
import schools from './schoolsSlice';
import staffType from './staffTypeSlice';
import staffTypes from './staffTypesSlice';
import documentTypes from './documentTypesSlice';
import documentType from './documentTypeSlice';
import categories from './categoriesSlice';

const reducer = combineReducers({
  schoolTypes,
  schoolType,
  schools,
  school,
  staffTypes,
  staffType,
  documentTypes,
  documentType,
  categories,
});

export default reducer;
