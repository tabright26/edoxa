import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from ".";
import store from "store";
import Input from "components/Shared/Input";
import {
  PROFILE_FIRST_NAME_REQUIRED,
  PROFILE_FIRST_NAME_INVALID
} from "utils/form/validators";
import {
  findFieldByName,
  findSubmitButton,
  findCancelButton,
  findInputByName,
  findFormFeedback
} from "test/helper";

const shallow = global["shallow"];
const mount = global["mount"];

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Update handleCancel={() => {}} />
    </Provider>
  );
};

describe("<UserInformationUpdateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Update handleCancel={() => {}} />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines information update form fields", () => {
    it("renders firstName field", () => {
      const wrapper = createWrapper();
      const field = findFieldByName(wrapper, "firstName");

      expect(field.prop("placeholder")).toBe("First name");
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders submit button", () => {
      const wrapper = createWrapper();
      const submitButton = findSubmitButton(wrapper);

      expect(submitButton.prop("type")).toBe("submit");
      expect(submitButton.text()).toBe("Save");
    });

    it("renders cancel button", () => {
      const wrapper = createWrapper();
      const cancelButton = findCancelButton(wrapper);

      expect(cancelButton.prop("type")).toBe("button");
      expect(cancelButton.text()).toBe("Cancel");
    });
  });

  describe("form validation", () => {
    describe("firstName validation", () => {
      it("shows error when firstName is set to blank", () => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, "firstName");
        input.simulate("blur");

        const errorPresent = findFormFeedback(
          wrapper,
          PROFILE_FIRST_NAME_REQUIRED
        );
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when firstName is set to invalid", () => {
        const wrapper = createWrapper();
        const input = findInputByName(wrapper, "firstName");
        input.simulate("change", { target: { value: "_123" } });

        const errorPresent = findFormFeedback(
          wrapper,
          PROFILE_FIRST_NAME_INVALID
        );
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
