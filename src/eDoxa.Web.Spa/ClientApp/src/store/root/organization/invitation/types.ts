import { AxiosState } from "utils/axios/types";
import { Invitation } from "types/clans";

export type ClanInvitationsState = AxiosState<Invitation[]>;
