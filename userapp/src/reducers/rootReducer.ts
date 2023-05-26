import { combineReducers } from 'redux';
import userReducer from './userReducer';
import adminReducer from './adminReducer';
// import other reducers as needed

const rootReducer = combineReducers({
  user: userReducer,
  admin: adminReducer
});

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
