import { AxiosState } from "utils/axios/types";
import { UserAddress } from "types";

export type UserAddressBookState = AxiosState<UserAddress[]>;
