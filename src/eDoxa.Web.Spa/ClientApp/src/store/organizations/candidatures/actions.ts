import { LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL, CandidaturesActionCreators } from "./types";

export function loadCandidatures(): CandidaturesActionCreators {
  return {
    types: [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/organizations/api/candidatures"
      }
    }
  };
}
