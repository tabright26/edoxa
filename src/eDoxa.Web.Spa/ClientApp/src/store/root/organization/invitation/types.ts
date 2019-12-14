import { AxiosState } from "utils/axios/types";
import { Invitation } from "types";

export type ClanInvitationsState = AxiosState<Invitation[]>;
