import { AxiosState } from "utils/axios/types";
import { Address } from "types/identity";

export type UserAddressBookState = AxiosState<Address[]>;
