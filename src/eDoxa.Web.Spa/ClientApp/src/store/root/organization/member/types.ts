import { AxiosState } from "utils/axios/types";
import { ClanMember } from "types/clans";

export type ClanMembersState = AxiosState<ClanMember[]>;
