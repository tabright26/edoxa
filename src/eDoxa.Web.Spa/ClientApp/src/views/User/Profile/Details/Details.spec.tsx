import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Details from "./Details";
import { UserAddressBookState } from "store/root/user/addressBook/types";
import { UserEmailState } from "store/root/user/email/types";
import { UserInformationsState } from "store/root/user/informations/types";

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

  const email: UserEmailState = {
    data: { address: "gabriel@edoxa.gg", verified: true },
    loading: false,
    error: null
  };

  const informations: UserInformationsState = {
    data: { firstName: "Gabriel", lastName: "Roy", gender: "Male", dob: { year: 1995, month: 8, day: 4 } },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            addressBook,
            email,
            informations
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
        <Details />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
