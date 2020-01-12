import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Update from "./Update";
import { configureStore } from "store";
import { FormGroup } from "reactstrap";
import Input from "components/Shared/Input";

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

describe("<UserPhoneUpdateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Update handleCancel={() => {}} />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines phone update form fields", () => {
    it("renders number field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("number");

      expect(field.prop("type")).toBe("text");
      expect(field.prop("label")).toBe("Phone Number");
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
});
