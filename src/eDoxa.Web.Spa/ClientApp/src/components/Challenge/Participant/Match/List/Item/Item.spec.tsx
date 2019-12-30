import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import Item from "./Item";
import { ChallengesState } from "store/root/challenge/types";

it("renders without crashing", () => {
  //Arrange
  const challenges: ChallengesState = {
    data: [
      {
        id: "123",
        participants: [
          {
            id: "id1",
            matches: [{ id: "1", stats: [] }]
          },
          {
            id: "id2",
            matches: [{ id: "2", stats: [] }]
          },
          {
            id: "id3",
            matches: [{ id: "3", stats: [] }]
          }
        ]
      },
      {
        id: "456",
        participants: [
          {
            id: "id1",
            matches: [{ id: "1", stats: [] }]
          },
          {
            id: "id2",
            matches: [{ id: "2", stats: [] }]
          },
          {
            id: "id3",
            matches: [{ id: "3", stats: [] }]
          }
        ]
      },
      {
        id: "789",
        participants: [
          {
            id: "id1",
            matches: [{ id: "1", stats: [] }]
          },
          {
            id: "id2",
            matches: [{ id: "2", stats: [] }]
          },
          {
            id: "id3",
            matches: [{ id: "3", stats: [] }]
          }
        ]
      }
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
  const participant = challenge.participants.find(participant => participant.id === "id1");
  const match = participant.matches.find(match => match.id === "1");

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Item match={match} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
