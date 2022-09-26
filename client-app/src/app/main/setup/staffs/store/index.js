import { combineReducers } from '@reduxjs/toolkit';
import staffs from './staffsSlice';
import staff from './staffSlice';

const reducer = combineReducers({
  staffs,
  staff,
});

export default reducer;
