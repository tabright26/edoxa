import { IAxiosAction } from "interfaces/axios";
import { LoadLeagueOfLegendsSummonerByNameActionType } from "actions/arena/games/leagueOfLegends/creators";

export const initialState = {};

export const reducer = (state = initialState, action: IAxiosAction<LoadLeagueOfLegendsSummonerByNameActionType>) => {
  switch (action.type) {
    case "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS":
      return action.payload.data;
    case "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL":
    default:
      return state;
  }
};
