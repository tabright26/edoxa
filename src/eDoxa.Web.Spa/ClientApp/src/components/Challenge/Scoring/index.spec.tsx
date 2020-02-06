import React from "react";
import renderer from "react-test-renderer";
import Scoring from ".";
//import { ChallengesState } from "store/root/challenge/types";
import { MemoryRouter } from "react-router-dom";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <MemoryRouter>
        <Scoring />
      </MemoryRouter>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
