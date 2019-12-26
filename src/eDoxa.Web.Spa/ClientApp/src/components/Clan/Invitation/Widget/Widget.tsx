import React from "react";
import { toastr } from "react-redux-toastr";
import { Col } from "reactstrap";

import InvitationForm from "../Form";

const InvitationWidget = ({
  actions,
  invitations: { data },
  doxatags,
  clanId
}) => {
  const findDoxatag = (name, code) => {
    return doxatags.find(tag => tag.name === name && tag.code === code);
  };

  const handleAddInvitation = data => {
    var alreadyExist = data.some(
      invitation => invitation.userId === data.userId
    );

    if (alreadyExist) {
      toastr.error("Error", "Invitation already exist.");
    } else {
      var userIdFound = findDoxatag(data.name, data.code);
      if (userIdFound) {
        actions
          .addInvitation(clanId, userIdFound.userId)
          .then(
            toastr.success("SUCCESS", "Candidature was sent successfully.")
          );
      } else {
        toastr.error("Error", "User does not exist.");
      }
    }
  };

  return (
    <InvitationForm.Create
      initialValues={{ clanId: clanId }}
      invitations={data}
      onSubmit={data => handleAddInvitation(data)}
    />
  );
};

export default InvitationWidget;
