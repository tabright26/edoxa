import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Reset from ".";
import store from "store";
import Input from "components/Shared/Input";
import { MemoryRouter } from "react-router-dom";
import { findFieldByName, findSubmitButton } from "test/helper";

const shallow = global["shallow"];
const mount = global["mount"];

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <MemoryRouter>
        <Reset />
      </MemoryRouter>
    </Provider>
  );
};

describe("<UserPasswordResetForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Reset />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines password reset form fields", () => {
    it("renders password field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "password");

      expect(field.prop("type")).toBe("password");
      expect(field.prop("placeholder")).toBe("New password");
      expect(field.prop("component")).toBe(Input.Password);
    });

    it("renders confirmPassword field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "newPassword");

      expect(field.prop("type")).toBe("password");
      expect(field.prop("placeholder")).toBe("Confirm new password");
      expect(field.prop("component")).toBe(Input.Password);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = findSubmitButton(wrapper);

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Reset");
    });
  });
});
