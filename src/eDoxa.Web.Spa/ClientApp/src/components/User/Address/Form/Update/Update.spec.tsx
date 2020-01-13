import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from "./Update";
import { configureStore } from "store";
import { FormGroup } from "reactstrap";
import Input from "components/Shared/Input";
import {
  ADDRESS_LINE1_REQUIRED,
  ADDRESS_LINE1_INVALID,
  ADDRESS_LINE2_INVALID,
  ADDRESS_CITY_REQUIRED,
  ADDRESS_CITY_INVALID,
  ADDRESS_STATE_INVALID,
  ADDRESS_POSTAL_CODE_INVALID
} from "validation";

const shallow = global["shallow"];
const mount = global["mount"];

const store = configureStore();

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Update addressId="addressId" handleCancel={() => {}} />
    </Provider>
  );
};

describe("<UserAddressUpdateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Update addressId="addressId" handleCancel={() => {}} />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines address update form fields", () => {
    it("renders country field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("country");

      expect(field.prop("type")).toBe("select");
      expect(field.prop("component")).toBe(Input.Select);
    });

    test.each([
      ["line1", "Address line 1"],
      ["line2", "Address line 2 (optional)"],
      ["city", "City"],
      ["state", "State"],
      ["postalCode", "Postal Code"]
    ])("renders name fields", (name: string, label: string) => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName(name);

      expect(field.prop("type")).toBe("text");
      expect(field.prop("label")).toBe(label);
      expect(field.prop("formGroup")).toBe(FormGroup);
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
    describe("text fields validation", () => {
      test.each([
        ["line1", ADDRESS_LINE1_REQUIRED],
        ["city", ADDRESS_CITY_REQUIRED]
      ])("name fields blank validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName(name);
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(message);
        expect(errorPresent).toBeTruthy();
      });

      test.each([
        ["line1", ADDRESS_LINE1_INVALID],
        ["line2", ADDRESS_LINE2_INVALID],
        ["city", ADDRESS_CITY_INVALID],
        ["state", ADDRESS_STATE_INVALID],
        ["postalCode", ADDRESS_POSTAL_CODE_INVALID]
      ])("name fields blank validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName(name);
        input.simulate("change", { target: { value: "_!" } });

        const errorPresent = wrapper.findFormFeedback(message);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});
