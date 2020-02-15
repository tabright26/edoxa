import { AxiosState } from "utils/axios/types";
import { UserAddress } from "types/identity";

export type UserAddressBookState = AxiosState<UserAddress[]>;
