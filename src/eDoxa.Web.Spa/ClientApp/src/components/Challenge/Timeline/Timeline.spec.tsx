import React from "react";
import renderer from "react-test-renderer";
import Timeline from "./Timeline";
import { ChallengeTimeline } from "types";

it("renders without crashing", () => {
  // Arrange

  const state: string = "";
  const timeline: ChallengeTimeline = {
    createdAt: Date.now(),
    startedAt: null,
    endedAt: null,
    closedAt: null
  };

  // Act
  const tree = renderer.create(<Timeline state={state} timeline={timeline} />).toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
