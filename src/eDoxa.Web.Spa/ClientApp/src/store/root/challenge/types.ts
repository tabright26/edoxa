import { AxiosState } from "utils/axios/types";
import { Challenge } from "types";

export type ChallengesState = AxiosState<Challenge[]>;
