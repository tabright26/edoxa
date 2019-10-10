import React from "react";
import { toastr } from "react-redux-toastr";
import { Col } from "reactstrap";

import InvitationForm from "forms/Organizations/Invitations";

const InvitationWidget = ({ actions, invitations, doxaTags, clanId }) => {
  const findDoxaTag = (name, code) => {
    return doxaTags.find(tag => tag.name === name && tag.code === code);
  };

  const handleAddInvitation = data => {
    var alreadyExist = invitations.some(invitation => invitation.userId === data.userId);

    if (alreadyExist) {
      toastr.error("Error", "Invitation already exist.");
    } else {
      var userIdFound = findDoxaTag(data.name, data.code);
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
