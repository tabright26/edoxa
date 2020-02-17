import React from "react";
import renderer from "react-test-renderer";
import Payout from ".";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import store from "store";
import { createChallenge } from "test/service/challenges";
import { createAction } from "test/helper";
import { LOAD_CHALLENGE_SUCCESS } from "store/actions/challenge/types";

it("renders without crashing", () => {
  const challenge = createChallenge();
  const action = createAction(LOAD_CHALLENGE_SUCCESS, challenge);
  store.dispatch(action);

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Payout challengeId={challenge.id} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
