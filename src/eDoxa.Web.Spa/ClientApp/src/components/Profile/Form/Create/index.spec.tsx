import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Create from ".";
import { configureStore } from "store";
import Input from "components/Shared/Input";
import {
  PROFILE_FIRST_NAME_REQUIRED,
  PROFILE_FIRST_NAME_INVALID,
  PROFILE_LAST_NAME_REQUIRED,
  PROFILE_LAST_NAME_INVALID
} from "utils/form/validators";
import {
  findFieldByName,
  findSubmitButton,
  findInputByName,
  findFormFeedback
} from "utils/test/helpers";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Create handleCancel={() => {}} />
    </Provider>
  );
};

describe("<UserInformationCreateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Create handleCancel={() => {}} />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines information create form fields", () => {
    test.each([
      ["firstName", "First Name"],
      ["lastName", "Last Name"]
    ])("renders name fields", (name: string, label: string) => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, name);

      expect(field.prop("placeholder")).toBe(label);
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders gender field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "gender");

      expect(field.prop("type")).toBe("select");
      expect(field.prop("component")).toBe(Input.Select);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = findSubmitButton(wrapper);

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Save");
    });
  });

  describe("form validation", () => {
    describe("name fields validation", () => {
      test.each([
        ["firstName", PROFILE_FIRST_NAME_REQUIRED],
        ["lastName", PROFILE_LAST_NAME_REQUIRED]
      ])("name fields blank validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, name);
        input.simulate("blur");

        const errorPresent = findFormFeedback(wrapper, message);
        expect(errorPresent).toBeTruthy();
      });

      test.each([
        ["firstName", PROFILE_FIRST_NAME_INVALID],
        ["lastName", PROFILE_LAST_NAME_INVALID]
      ])("name fields invalid validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, name);
        input.simulate("change", { target: { value: "_123" } });

        const errorPresent = findFormFeedback(wrapper, message);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
