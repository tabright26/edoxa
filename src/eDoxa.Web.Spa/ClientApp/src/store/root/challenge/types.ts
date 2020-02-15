import { AxiosState } from "utils/axios/types";
import { Challenge } from "types/challenges";

export type ChallengesState = AxiosState<Challenge[]>;
