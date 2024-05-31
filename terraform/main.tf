terraform {
  required_version = ">= 0.13"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 3.0"
    }
  }
}

provider "aws" {
  region = "eu-west-1"
}

resource "aws_key_pair" "dj_kp" {
  key_name = "dj_kp"

  tags = {
    name = "dj_kp"
    owner = "cameron.worthington@bbd.co.za"
    created-using = "terraform"
  }
}

data "template_file" "user_data" {
  template = file("install-dotnet.sh")
}

resource "aws_instance" "dj_api" {
  ami = var.ec2_ami
  count = var.ec2_count
  instance_type = var.ec2_instance_type
  key_name = aws_key_pair.dj_kp.key_name
  security_groups = [element(var.ec2_sg, count.index)]
  subnet_id = var.ec2_subnet_id

  # provisioner "file" {
  #   source      = "install-dotnet.sh"  # Path to your script file
  #   destination = "/tmp/install-dotnet.sh"     # Destination path on the EC2 instance

  #   connection {
  #     type        = "ssh"
  #     user        = "ubuntu"
  #     private_key = file("dj_kp")  # Path to your private key
  #     host        = self.public_ip
  #   }
  # }

  # provisioner "remote-exec" {
  #   inline = [
  #     "chmod +x /tmp/install-dotnet.sh",       # Make the script executable
  #     "/tmp/install-dotnet.sh"                 # Execute the script
  #   ]

  #   connection {
  #     type        = "ssh"
  #     user        = "ubuntu"
  #     private_key = file("dj_kp")  # Path to your private key
  #     host        = self.public_ip
  #   }
  # }

  user_data = data.template_file.user_data.rendered

  tags = {
    Name = "dj_api"
    owner = "cameron.worthington@bbd.co.za"
    created-using = "terraform"
  }
}
