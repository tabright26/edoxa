import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import AddressBook from "./AddressBook";
import { UserAddressBookState } from "store/root/user/addressBook/types";

it("renders without crashing", () => {
  //Arrange
  const addressBook: UserAddressBookState = {
    data: [
      {
        country: "Canada",
        line1: "test address",
        city: "Montreal",
        id: "1"
      },
      {
        country: "Canada",
        line1: "test address",
        city: "Quebec",
        id: "2"
      }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            addressBook
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
        <AddressBook />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
