pipeline {
    agent any

    environment {
        DOTNET_VERSION = '8.0'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    def dotnetCmd = "dotnet build -c Release"
                    bat(dotnetCmd)
                    echo 'Test'
                }
            }
        }

        stage('Docker build'){
            steps{
                script{
                    def dockerBuild = "docker build -t bio.tree:1.0 -f Dockerfile ."
                    bat(dockerBuild)
                }
            }
        }
    }

    post {
        success {
            echo 'Finished with success'
        }
        failure {
            echo 'Build has errors'
        }
    }
}
