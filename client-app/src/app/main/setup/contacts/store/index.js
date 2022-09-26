import { combineReducers } from '@reduxjs/toolkit';
import tags from './tagsSlice';
import contacts from './contactsSlice';
import schools from './schoolsSlice';
import contact from './contactSlice';
import roles from './rolesSlice';

const reducer = combineReducers({
  tags,
  schools,
  contacts,
  contact,
  roles,
});

export default reducer;
