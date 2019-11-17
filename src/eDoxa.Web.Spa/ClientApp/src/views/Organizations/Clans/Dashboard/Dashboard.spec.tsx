import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Dashboard from "./Dashboard";
import { ClansState } from "store/root/organization/clan/types";

it("renders without crashing", () => {
  //Arrange
  const clans: ClansState = {
    data: [
      {
        name: "Clan 1",
        clanId: "1",
        members: [{ userId: "123123123" }]
      },
      {
        name: "Clan 2",
        clanId: "2",
        members: [{ userId: "321321321" }]
      },
      {
        name: "Clan 3",
        clanId: "3",
        members: [{ userId: "123456789" }]
      }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        oidc: {
          user: {
            profile: {
              sub: "123123123"
            }
          }
        },
        root: {
          organization: {
            clan: clans
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Dashboard
          match={{ params: "1", isExact: false, path: "", url: "" }}
          history={null}
          location={null}
        />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
