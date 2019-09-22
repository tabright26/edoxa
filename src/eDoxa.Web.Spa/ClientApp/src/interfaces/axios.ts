import { AxiosRequestConfig } from "axios";

export interface IAxiosPayload {
  client?: string;
  request: AxiosRequestConfig;
}
export interface IAxiosActionCreator<TActionType> {
  types: TActionType[];
  payload: IAxiosPayload;
}
