import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from "./Update";
import { configureStore } from "store";
import Input from "components/Shared/Input";
import {
  PERSONALINFO_FIRSTNAME_REQUIRED,
  PERSONALINFO_FIRSTNAME_INVALID
} from "validation";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

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
      const field = wrapper.findFieldByName("firstName");

      expect(field.prop("label")).toBe("First Name");
      expect(field.prop("component")).toBe(Input.Text);
    });

    it("renders save button", () => {
      const wrapper = createWrapper();
      const saveButton = wrapper.findSaveButton();

      expect(saveButton.prop("type")).toBe("submit");
      expect(saveButton.text()).toBe("Save");
    });

    it("renders cancel button", () => {
      const wrapper = createWrapper();
      const cancelButton = wrapper.findCancelButton();

      expect(cancelButton.prop("type")).toBe("button");
      expect(cancelButton.text()).toBe("Cancel");
    });
  });

  describe("form validation", () => {
    describe("firstName validation", () => {
      it("shows error when firstName is set to blank", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("firstName");
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(
          PERSONALINFO_FIRSTNAME_REQUIRED
        );
        expect(errorPresent).toBeTruthy();
      });

      it("shows error when firstName is set to invalid", () => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName("firstName");
        input.simulate("change", { target: { value: "_123" } });

        const errorPresent = wrapper.findFormFeedback(
          PERSONALINFO_FIRSTNAME_INVALID
        );
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
