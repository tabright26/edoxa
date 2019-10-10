import { AxiosActionCreator, AxiosAction } from "store/types";

export const LOAD_LOGO = "LOAD_LOGO";
export const LOAD_LOGO_SUCCESS = "LOAD_LOGO_SUCCESS";
export const LOAD_LOGO_FAIL = "LOAD_LOGO_FAIL";

export const UPDATE_LOGO = "UPDATE_LOGO";
export const UPDATE_LOGO_SUCCESS = "UPDATE_LOGO_SUCCESS";
export const UPDATE_LOGO_FAIL = "UPDATE_LOGO_FAIL";

type LoadLogoType = typeof LOAD_LOGO | typeof LOAD_LOGO_SUCCESS | typeof LOAD_LOGO_FAIL;

interface LoadLogoActionCreator extends AxiosActionCreator<LoadLogoType> {}

interface LoadLogoAction extends AxiosAction<LoadLogoType> {}

//----------------------------------------------------------------------------------------------

type UpdateLogoType = typeof UPDATE_LOGO | typeof UPDATE_LOGO_SUCCESS | typeof UPDATE_LOGO_FAIL;

interface UpdateLogoActionCreator extends AxiosActionCreator<UpdateLogoType> {}

interface UpdateLogoAction extends AxiosAction<UpdateLogoType> {}

//----------------------------------------------------------------------------------------------

export type LogoActionCreators = LoadLogoActionCreator | UpdateLogoActionCreator;

export type LogoActionTypes = LoadLogoAction | UpdateLogoAction;

export interface LogoState {}
