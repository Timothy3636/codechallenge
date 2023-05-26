// Define action types
export const REGISTER_USER = 'REGISTER_USER';
export const USER_LOGIN = 'USER_LOGIN';
export const SET_USER = 'SET_USER';
export const REGISTER_ADMIN = 'REGISTER_ADMIN';
export const ADMIN_LOGIN = 'ADMIN_LOGIN';
export const SET_ADMIN = 'SET_ADMIN';

export const GET_FX_RATE = 'GET_FX_RATE';
export const MAKE_CONTRACT = 'MAKE_CONTRACT';
export const MARK_EXECUTE = 'MARK_EXECUTE';
export const LIST_CONTRACT = 'LIST_CONTRACT';

export interface SetAdminAction {
  type: typeof SET_ADMIN;
  payload: Admin | null;
}

export interface AdminLoginAction {
  type: typeof ADMIN_LOGIN;
  payload: string;
}

export interface RegisterUserAction {
  type: typeof REGISTER_USER;
  payload: User;
}

// Define the user action interfaces
export interface SetUserAction {
  type: typeof SET_USER;
  payload: User | null;
}

export interface UserLoginAction {
  type: typeof USER_LOGIN;
  payload: string;
}

export interface RegisterUserAction {
  type: typeof REGISTER_USER;
  payload: User;
}


export interface MarkExecuteAction {
  type: typeof MARK_EXECUTE;
  payload: number;
}


export interface ListContractAction {
  type: typeof LIST_CONTRACT;
  payload: RequestContract[];
}



export interface GetFxRateAction {
  type: typeof GET_FX_RATE;
  payload: FxRate;
}

// Define action interfaces
export  interface MakeContractAction {
  type: typeof MAKE_CONTRACT;
  payload: number;
}


// Define union type for all user action types
export type actionTypes = 
RegisterUserAction | 
GetFxRateAction | 
MakeContractAction | 
MarkExecuteAction |
UserLoginAction |
SetUserAction | 
ListContractAction | 
AdminLoginAction |
SetAdminAction 
; 



// Define user base interface
export interface UserBase {
  id? : number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}


// Define user interface
export interface User extends UserBase {
  userId? : number;
  registrationDate?: Date;
}

export interface Admin extends UserBase {
  adminId? : number;
  role?: string;
  addedDate?: Date;

}



export interface RequestContract {
  UserID?: number;
  ConvertToCurrency: string;
  FundFromCurrency: string;
  FundFromAmount: number;
  ConvertToAmount: number;
  ExchangeRate: number;
  ContacttId?: number;
}


export interface FxRate {
  denominator: string;
  numerator: string;
  rate: number | string;
  lastUpdateDate: Date;
}


