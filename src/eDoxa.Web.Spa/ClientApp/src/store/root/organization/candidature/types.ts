import { AxiosState } from "utils/axios/types";
import { Candidature } from "types/clans";

export type ClanCandidaturesState = AxiosState<Candidature[]>;
