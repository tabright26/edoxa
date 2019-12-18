import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Address } from "types";

export type UserAddressBookState = AxiosState<Address[]>;
