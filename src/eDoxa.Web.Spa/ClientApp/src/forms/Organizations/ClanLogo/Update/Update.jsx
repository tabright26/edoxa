import React from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import ImageUploader from "react-images-upload";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { UPDATE_CLAN_LOGO_FORM } from "forms";
import validate from "./validate";

class Image extends React.Component {
  constructor(props) {
    super(props);
    this.state = { pictures: [] };
    this.onDrop = this.onDrop.bind(this);
  }

  onDrop(picture) {
    this.setState({
      pictures: this.state.pictures.concat(picture)
    });
  }

  render() {
    return <ImageUploader withIcon={true} name="logo" buttonText="Choose images" onChange={this.onDrop} imgExtension={[".jpg", ".png"]} maxFileSize={5242880} />;
  }
}

const UpdateClanLogoForm = ({ handleSubmit, handleCancel, initialValues: { clanId, logo } }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <FormGroup className="mb-0">
      <Input.Text type="hidden" value={clanId} name="clanId" disabled />
      <Image />
      <Button.Submit width="50px" color="info">
        Update
      </Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_CLAN_LOGO_FORM, validate })(UpdateClanLogoForm);
