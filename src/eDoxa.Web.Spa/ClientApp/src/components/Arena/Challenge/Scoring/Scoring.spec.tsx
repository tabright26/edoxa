import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router";
import Scoring from "./Scoring";
import { ChallengesState } from "store/root/arena/challenges/types";

it("renders without crashing", () => {
  //Arrange
  const challenges: ChallengesState = {
    data: [
      { id: "123", participants: [{ id: "id1", matches: [] }, { id: "id2", matches: [] }, { id: "id3", matches: [] }] },
      { id: "456", participants: [{ id: "id1", matches: [] }, { id: "id2", matches: [] }, { id: "id3", matches: [] }] },
      { id: "789", participants: [{ id: "id1", matches: [] }, { id: "id2", matches: [] }, { id: "id3", matches: [] }] }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  const challenge = challenges.data.find(challenge => challenge.id === "123");

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Scoring challenge={challenge} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
