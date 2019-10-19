import React from "react";
import { toastr } from "react-redux-toastr";
import { Col } from "reactstrap";

import InvitationForm from "forms/Organization/Invitation";

const InvitationWidget = ({ actions, invitations, doxatags, clanId }) => {
  const findDoxatag = (name, code) => {
    return doxatags.find(tag => tag.name === name && tag.code === code);
  };

  const handleAddInvitation = data => {
    var alreadyExist = invitations.some(invitation => invitation.userId === data.userId);

    if (alreadyExist) {
      toastr.error("Error", "Invitation already exist.");
    } else {
      var userIdFound = findDoxatag(data.name, data.code);
      if (userIdFound) {
        actions.addInvitation(clanId, userIdFound.userId).then(toastr.success("SUCCESS", "Candidature was sent successfully."));
      } else {
        toastr.error("Error", "User does not exist.");
      }
    }
  };

  return (
    <Col>
      <InvitationForm.Create initialValues={{ clanId: clanId }} invitations={invitations} onSubmit={data => handleAddInvitation(data)} />
    </Col>
  );
};

export default InvitationWidget;
