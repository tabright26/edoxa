import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import List from ".";
import store from "store";
import {
  createChallenge,
  createChallengeParticipant
} from "test/service/challenges";
import { LOAD_CHALLENGE_SUCCESS } from "store/actions/challenge/types";
import { createAction } from "test/helper";
import { GAME_LEAGUEOFLEGENDS } from "types/games";
import { CHALLENGE_STATE_INSCRIPTION } from "types/challenges";

it("renders without crashing", () => {
  // Arrange
  const challengeId = "ChallengeId";
  const challengeParticipant = createChallengeParticipant(challengeId);
  const challenge = createChallenge(
    challengeId,
    "ChallengeName",
    GAME_LEAGUEOFLEGENDS,
    CHALLENGE_STATE_INSCRIPTION,
    3,
    10,
    [challengeParticipant]
  );
  const action = createAction(LOAD_CHALLENGE_SUCCESS, challenge);
  store.dispatch(action);

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <List
            challengeId={challenge.id}
            participantId={challengeParticipant.id}
          />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
